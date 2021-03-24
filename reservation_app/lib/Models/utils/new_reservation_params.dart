import 'package:reservation_app/Models/restaurants/open_hours.dart';

class NewReservationParams {
  NewReservationParams(this.restaurantId, this.schedule);

  final int restaurantId;
  final List<OpenHours> schedule;
}
