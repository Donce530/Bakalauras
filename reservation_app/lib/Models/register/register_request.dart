import 'package:json_annotation/json_annotation.dart';
import 'package:reservation_app/Models/interfaces/json_format.dart';

part 'register_request.g.dart';

@JsonSerializable()
class RegisterRequest implements JsonFormat {
  RegisterRequest(this.firstName, this.lastname, this.phoneNumber, this.email,
      this.password);

  final String firstName;
  final String lastname;
  final String phoneNumber;
  final String email;
  final String password;

  factory RegisterRequest.fromJson(Map<String, dynamic> json) =>
      _$RegisterRequestFromJson(json);

  @override
  Map<String, dynamic> toJson() => _$RegisterRequestToJson(this);
}
