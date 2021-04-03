import 'package:json_annotation/json_annotation.dart';
import 'package:reservation_app/Models/interfaces/json_format.dart';
import 'package:reservation_app/Models/restaurants/plan/plan_table.dart';
import 'package:reservation_app/Models/restaurants/plan/plan_wall.dart';

part 'restaurant_plan.g.dart';

@JsonSerializable(explicitToJson: true)
class RestaurantPlan implements JsonFormat {
  RestaurantPlan(this.id, this.webSvg, this.walls, this.tables);

  final int id;
  final String webSvg;
  final List<PlanWall> walls;
  final List<PlanTable> tables;

  factory RestaurantPlan.fromJson(Map<String, dynamic> json) => _$RestaurantPlanFromJson(json);

  Map<String, dynamic> toJson() => _$RestaurantPlanToJson(this);
}
