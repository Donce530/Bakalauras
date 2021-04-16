import 'package:json_annotation/json_annotation.dart';
import 'package:reservation_app/Models/interfaces/json_format.dart';
import 'package:reservation_app/Models/restaurants/plan/plan_item_base.dart';

part 'plan_label.g.dart';

@JsonSerializable()
class PlanLabel extends PlanItemBase implements JsonFormat {
  PlanLabel(
      int id, String svg, double x, double y, double width, double height, this.text, this.fontSize)
      : super(id, svg, x, y, width, height);

  final String text;
  final double fontSize;

  factory PlanLabel.fromJson(Map<String, dynamic> json) => _$PlanLabelFromJson(json);

  Map<String, dynamic> toJson() => _$PlanLabelToJson(this);
}
