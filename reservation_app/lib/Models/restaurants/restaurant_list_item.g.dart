// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'restaurant_list_item.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

RestaurantListItem _$RestaurantListItemFromJson(Map<String, dynamic> json) {
  return RestaurantListItem(
    json['id'] as int,
    json['title'] as String,
    json['address'] as String,
  );
}

Map<String, dynamic> _$RestaurantListItemToJson(RestaurantListItem instance) =>
    <String, dynamic>{
      'id': instance.id,
      'title': instance.title,
      'address': instance.address,
    };
