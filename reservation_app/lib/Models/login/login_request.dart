import 'package:json_annotation/json_annotation.dart';
import 'package:reservation_app/Models/interfaces/json_format.dart';

part 'login_request.g.dart';

@JsonSerializable()
class LoginRequest implements JsonFormat {
  LoginRequest(this.email, this.password);

  final String email;
  final String password;

  factory LoginRequest.fromJson(Map<String, dynamic> json) =>
      _$LoginRequestFromJson(json);

  Map<String, dynamic> toJson() => _$LoginRequestToJson(this);
}
