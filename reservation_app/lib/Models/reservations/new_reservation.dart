import 'package:json_annotation/json_annotation.dart';
import 'package:reservation_app/Models/interfaces/json_format.dart';

part 'new_reservation.g.dart';

@JsonSerializable()
class NewReservation implements JsonFormat {
  NewReservation(this.restaurantId, this.day, this.start, this.end, this.tableId);

  final int restaurantId;
  final int tableId;
  final DateTime day;
  final DateTime start;
  final DateTime end;

  factory NewReservation.fromJson(Map<String, dynamic> json) => _$NewReservationFromJson(json);

  Map<String, dynamic> toJson() => _$NewReservationToJson(this);
}
