import 'package:json_annotation/json_annotation.dart';
import 'package:reservation_app/Models/interfaces/json_format.dart';

part 'restaurant_list_item.g.dart';

@JsonSerializable()
class RestaurantListItem implements JsonFormat {
  RestaurantListItem(this.id, this.title, this.address);

  final int id;
  final String title;
  final String address;

  factory RestaurantListItem.fromJson(Map<String, dynamic> json) =>
      _$RestaurantListItemFromJson(json);

  Map<String, dynamic> toJson() => _$RestaurantListItemToJson(this);
}
