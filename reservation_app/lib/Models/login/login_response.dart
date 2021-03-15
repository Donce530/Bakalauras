import 'package:json_annotation/json_annotation.dart';
import 'package:reservation_app/Models/interfaces/json_format.dart';

part 'login_response.g.dart';

@JsonSerializable()
class LoginResponse implements JsonFormat {
  LoginResponse(this.id, this.firstName, this.lastName, this.email, this.token);

  final int id;
  final String firstName;
  final String lastName;
  final String email;
  final String token;

  factory LoginResponse.fromJson(Map<String, dynamic> json) =>
      _$LoginResponseFromJson(json);

  Map<String, dynamic> toJson() => _$LoginResponseToJson(this);
}
