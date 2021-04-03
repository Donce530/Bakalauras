import 'package:json_annotation/json_annotation.dart';
import 'package:reservation_app/Models/interfaces/json_format.dart';
import 'package:reservation_app/Models/restaurants/plan/plan_item_base.dart';

part 'plan_wall.g.dart';

@JsonSerializable()
class PlanWall extends PlanItemBase implements JsonFormat {
  PlanWall(int id, String svg, double x, double y, double width, double height)
      : super(id, svg, x, y, width, height);

  factory PlanWall.fromJson(Map<String, dynamic> json) => _$PlanWallFromJson(json);

  Map<String, dynamic> toJson() => _$PlanWallToJson(this);
}
