// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'register_request.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

RegisterRequest _$RegisterRequestFromJson(Map<String, dynamic> json) {
  return RegisterRequest(
    json['firstName'] as String,
    json['lastname'] as String,
    json['phoneNumber'] as String,
    json['email'] as String,
    json['password'] as String,
  );
}

Map<String, dynamic> _$RegisterRequestToJson(RegisterRequest instance) =>
    <String, dynamic>{
      'firstName': instance.firstName,
      'lastname': instance.lastname,
      'phoneNumber': instance.phoneNumber,
      'email': instance.email,
      'password': instance.password,
    };
