// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'restaurant_plan.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

RestaurantPlan _$RestaurantPlanFromJson(Map<String, dynamic> json) {
  return RestaurantPlan(
    json['id'] as int,
    json['webSvg'] as String,
    (json['walls'] as List)
        ?.map((e) =>
            e == null ? null : PlanWall.fromJson(e as Map<String, dynamic>))
        ?.toList(),
    (json['tables'] as List)
        ?.map((e) =>
            e == null ? null : PlanTable.fromJson(e as Map<String, dynamic>))
        ?.toList(),
  );
}

Map<String, dynamic> _$RestaurantPlanToJson(RestaurantPlan instance) =>
    <String, dynamic>{
      'id': instance.id,
      'webSvg': instance.webSvg,
      'walls': instance.walls?.map((e) => e?.toJson())?.toList(),
      'tables': instance.tables?.map((e) => e?.toJson())?.toList(),
    };
