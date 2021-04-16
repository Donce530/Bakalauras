import 'package:json_annotation/json_annotation.dart';
import 'package:reservation_app/Models/interfaces/json_format.dart';
import 'package:reservation_app/Models/reservations/reservation_state.dart';

part 'reservation_list_item.g.dart';

@JsonSerializable()
class ReservationListItem implements JsonFormat {
  ReservationListItem(this.id, this.restaurantTitle, this.restaurantAddress, this.tableNumber,
      this.tableSeats, this.day, this.start, this.end, this.state);

  final int id;
  final String restaurantTitle;
  final String restaurantAddress;
  final int tableNumber;
  final int tableSeats;
  final DateTime day;
  final DateTime start;
  final DateTime end;
  final ReservationState state;

  @JsonKey(ignore: true)
  bool isExpanded = false;

  factory ReservationListItem.fromJson(Map<String, dynamic> json) =>
      _$ReservationListItemFromJson(json);

  Map<String, dynamic> toJson() => _$ReservationListItemToJson(this);
}
