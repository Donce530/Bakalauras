// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'plan_wall.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

PlanWall _$PlanWallFromJson(Map<String, dynamic> json) {
  return PlanWall(
    json['id'] as int,
    json['svg'] as String,
    (json['x'] as num)?.toDouble(),
    (json['y'] as num)?.toDouble(),
    (json['width'] as num)?.toDouble(),
    (json['height'] as num)?.toDouble(),
  );
}

Map<String, dynamic> _$PlanWallToJson(PlanWall instance) => <String, dynamic>{
      'id': instance.id,
      'svg': instance.svg,
      'x': instance.x,
      'y': instance.y,
      'width': instance.width,
      'height': instance.height,
    };
