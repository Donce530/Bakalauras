// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'plan_label.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

PlanLabel _$PlanLabelFromJson(Map<String, dynamic> json) {
  return PlanLabel(
    json['id'] as int,
    json['svg'] as String,
    (json['x'] as num)?.toDouble(),
    (json['y'] as num)?.toDouble(),
    (json['width'] as num)?.toDouble(),
    (json['height'] as num)?.toDouble(),
    json['text'] as String,
    (json['fontSize'] as num)?.toDouble(),
  );
}

Map<String, dynamic> _$PlanLabelToJson(PlanLabel instance) => <String, dynamic>{
      'id': instance.id,
      'svg': instance.svg,
      'x': instance.x,
      'y': instance.y,
      'width': instance.width,
      'height': instance.height,
      'text': instance.text,
      'fontSize': instance.fontSize,
    };
