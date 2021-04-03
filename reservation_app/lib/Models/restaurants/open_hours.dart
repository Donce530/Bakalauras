import 'package:json_annotation/json_annotation.dart';
import 'package:reservation_app/Models/interfaces/json_format.dart';

part 'open_hours.g.dart';

@JsonSerializable()
class OpenHours implements JsonFormat {
  OpenHours(this.open, this.close, this.weekDay);

  final DateTime open;
  final DateTime close;
  int weekDay;

  factory OpenHours.fromJson(Map<String, dynamic> json) => _$OpenHoursFromJson(json);

  Map<String, dynamic> toJson() => _$OpenHoursToJson(this);
}
