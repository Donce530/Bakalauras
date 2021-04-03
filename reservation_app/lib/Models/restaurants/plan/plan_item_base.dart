import 'package:json_annotation/json_annotation.dart';

@JsonSerializable(explicitToJson: true)
class PlanItemBase {
  PlanItemBase(this.id, this.svg, this.x, this.y, this.width, this.height);

  final int id;
  final String svg;
  final double x;
  final double y;
  final double width;
  final double height;

  @JsonKey(ignore: true)
  String scaledSvg;
}
