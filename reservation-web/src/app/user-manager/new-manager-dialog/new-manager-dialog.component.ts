import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { faUser } from '@fortawesome/free-regular-svg-icons';
import { faKey, faAt, faPhone } from '@fortawesome/free-solid-svg-icons';
import { MessageService } from 'primeng/api';
import { Role } from 'src/app/shared/enums/role.enum';
import { RegisterRequest } from '../models/register-request';
import { UserManagerDataService } from '../services/user-manager-data.service';

@Component({
  selector: 'app-new-manager-dialog',
  templateUrl: './new-manager-dialog.component.html',
  styleUrls: ['./new-manager-dialog.component.css']
})
export class NewManagerDialogComponent implements OnInit {

  @Output()
  closeDialog = new EventEmitter<boolean>();

  public visible = false;

  public icons = {
    user: faUser,
    key: faKey,
    email: faAt,
    phone: faPhone
  };
  public userForm: FormGroup;

  constructor(private _userManagerDataService: UserManagerDataService,
    private _messageService: MessageService) { }

  ngOnInit(): void {
    this.visible = true;
    this.userForm = new FormGroup({
      firstName: new FormControl(null, [Validators.required]),
      lastName: new FormControl(null, [Validators.required]),
      email: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl(null, [Validators.required, Validators.minLength(8)]),
      phoneNumber: new FormControl(null)
    });
  }

  public register(): void {
    const loginRequest: RegisterRequest = this.userForm.value;
    loginRequest.role = Role.Manager;
    this._userManagerDataService.registerUser(loginRequest).subscribe(
      () => {
        this._messageService.add({ severity: 'success', summary: 'Pavyko!', detail: 'Vadybininkas užregistruotas' });
        this.closeDialog.emit(true);
      },
      () => {
        this._messageService.add({ severity: 'error', summary: 'Klaida!', detail: 'El. pašto adresas jau naudojamas' });
      }
    );
  }

  public cancel(): void {
    console.log('cancale');
    this.closeDialog.emit(false);
  }

}
