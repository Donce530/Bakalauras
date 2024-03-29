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
    _$enumDecodeNullable(_$ReservationStateEnumMap, json['state']),
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
      'state': _$ReservationStateEnumMap[instance.state],
    };

T _$enumDecode<T>(
  Map<T, dynamic> enumValues,
  dynamic source, {
  T unknownValue,
}) {
  if (source == null) {
    throw ArgumentError('A value must be provided. Supported values: '
        '${enumValues.values.join(', ')}');
  }

  final value = enumValues.entries
      .singleWhere((e) => e.value == source, orElse: () => null)
      ?.key;

  if (value == null && unknownValue == null) {
    throw ArgumentError('`$source` is not one of the supported values: '
        '${enumValues.values.join(', ')}');
  }
  return value ?? unknownValue;
}

T _$enumDecodeNullable<T>(
  Map<T, dynamic> enumValues,
  dynamic source, {
  T unknownValue,
}) {
  if (source == null) {
    return null;
  }
  return _$enumDecode<T>(enumValues, source, unknownValue: unknownValue);
}

const _$ReservationStateEnumMap = {
  ReservationState.Created: 0,
  ReservationState.CheckedIn: 1,
  ReservationState.CheckedOut: 2,
};
