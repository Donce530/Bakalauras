// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'new_reservation.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

NewReservation _$NewReservationFromJson(Map<String, dynamic> json) {
  return NewReservation(
    json['restaurantId'] as int,
    json['day'] == null ? null : DateTime.parse(json['day'] as String),
    json['start'] == null ? null : DateTime.parse(json['start'] as String),
    json['end'] == null ? null : DateTime.parse(json['end'] as String),
    json['tableId'] as int,
  );
}

Map<String, dynamic> _$NewReservationToJson(NewReservation instance) =>
    <String, dynamic>{
      'restaurantId': instance.restaurantId,
      'tableId': instance.tableId,
      'day': instance.day?.toIso8601String(),
      'start': instance.start?.toIso8601String(),
      'end': instance.end?.toIso8601String(),
    };
