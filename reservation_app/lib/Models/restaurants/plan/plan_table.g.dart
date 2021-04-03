// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'plan_table.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

PlanTable _$PlanTableFromJson(Map<String, dynamic> json) {
  return PlanTable(
    json['id'] as int,
    json['svg'] as String,
    (json['x'] as num)?.toDouble(),
    (json['y'] as num)?.toDouble(),
    (json['width'] as num)?.toDouble(),
    (json['height'] as num)?.toDouble(),
    json['seats'] as int,
    json['number'] as int,
  );
}

Map<String, dynamic> _$PlanTableToJson(PlanTable instance) => <String, dynamic>{
      'id': instance.id,
      'svg': instance.svg,
      'x': instance.x,
      'y': instance.y,
      'width': instance.width,
      'height': instance.height,
      'seats': instance.seats,
      'number': instance.number,
    };
