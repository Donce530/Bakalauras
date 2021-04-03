// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'reservation_list_item.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

ReservationListItem _$ReservationListItemFromJson(Map<String, dynamic> json) {
  return ReservationListItem(
    json['id'] as int,
    json['restaurantTitle'] as String,
    json['restaurantAddress'] as String,
    json['tableNumber'] as int,
    json['tableSeats'] as int,
    json['day'] == null ? null : DateTime.parse(json['day'] as String),
    json['start'] == null ? null : DateTime.parse(json['start'] as String),
    json['end'] == null ? null : DateTime.parse(json['end'] as String),
  );
}

Map<String, dynamic> _$ReservationListItemToJson(
        ReservationListItem instance) =>
    <String, dynamic>{
      'id': instance.id,
      'restaurantTitle': instance.restaurantTitle,
      'restaurantAddress': instance.restaurantAddress,
      'tableNumber': instance.tableNumber,
      'tableSeats': instance.tableSeats,
      'day': instance.day?.toIso8601String(),
      'start': instance.start?.toIso8601String(),
      'end': instance.end?.toIso8601String(),
    };
