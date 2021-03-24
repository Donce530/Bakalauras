import 'package:json_annotation/json_annotation.dart';
import 'package:reservation_app/Models/interfaces/json_format.dart';
import 'package:reservation_app/Models/restaurants/open_hours.dart';

part 'restaurant_details.g.dart';

@JsonSerializable(explicitToJson: true)
class RestaurantDetails implements JsonFormat {
  RestaurantDetails(this.id, this.title, this.description, this.address, this.city, this.schedule);

  final int id;
  final String title;
  final String description;
  final String address;
  final String city;
  final List<OpenHours> schedule;

  factory RestaurantDetails.fromJson(Map<String, dynamic> json) =>
      _$RestaurantDetailsFromJson(json);

  Map<String, dynamic> toJson() => _$RestaurantDetailsToJson(this);
}
