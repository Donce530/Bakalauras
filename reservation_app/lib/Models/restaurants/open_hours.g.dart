// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'open_hours.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

OpenHours _$OpenHoursFromJson(Map<String, dynamic> json) {
  return OpenHours(
    json['open'] == null ? null : DateTime.parse(json['open'] as String),
    json['close'] == null ? null : DateTime.parse(json['close'] as String),
    json['weekDay'] as int,
  );
}

Map<String, dynamic> _$OpenHoursToJson(OpenHours instance) => <String, dynamic>{
      'open': instance.open?.toIso8601String(),
      'close': instance.close?.toIso8601String(),
      'weekDay': instance.weekDay,
    };
