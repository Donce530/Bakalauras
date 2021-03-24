// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'restaurant_details.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

RestaurantDetails _$RestaurantDetailsFromJson(Map<String, dynamic> json) {
  return RestaurantDetails(
    json['id'] as int,
    json['title'] as String,
    json['description'] as String,
    json['address'] as String,
    json['city'] as String,
    (json['schedule'] as List)
        ?.map((e) =>
            e == null ? null : OpenHours.fromJson(e as Map<String, dynamic>))
        ?.toList(),
  );
}

Map<String, dynamic> _$RestaurantDetailsToJson(RestaurantDetails instance) =>
    <String, dynamic>{
      'id': instance.id,
      'title': instance.title,
      'description': instance.description,
      'address': instance.address,
      'city': instance.city,
      'schedule': instance.schedule?.map((e) => e?.toJson())?.toList(),
    };
