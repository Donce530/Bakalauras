import 'package:json_annotation/json_annotation.dart';
import 'package:reservation_app/Models/interfaces/json_format.dart';
import 'package:reservation_app/Models/restaurants/plan/plan_item_base.dart';

part 'plan_table.g.dart';

@JsonSerializable()
class PlanTable extends PlanItemBase implements JsonFormat {
  PlanTable(
      int id, String svg, double x, double y, double width, double height, this.seats, this.number)
      : super(id, svg, x, y, width, height);

  final int seats;
  final int number;

  factory PlanTable.fromJson(Map<String, dynamic> json) => _$PlanTableFromJson(json);

  Map<String, dynamic> toJson() => _$PlanTableToJson(this);
}
