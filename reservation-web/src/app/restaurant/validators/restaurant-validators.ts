import { AbstractControl, ControlValueAccessor, ValidationErrors, ValidatorFn } from "@angular/forms";

export const openHoursValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
    const open = new Date(control.get('open').value);
    const close = new Date(control.get('close').value);

    let errors: ValidationErrors = null;

    if (open == null || close == null) {
        errors = { invalidSchedule: true };
    } else if (open.getHours() > close.getHours() ||
        open.getHours() === close.getHours() && open.getMinutes() > close.getMinutes()) {
        errors = { invalidSchedule: true };
    }

    return errors;
};
