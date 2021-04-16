import 'package:json_annotation/json_annotation.dart';

enum ReservationState {
  @JsonValue(0)
  Created,
  @JsonValue(1)
  CheckedIn,
  @JsonValue(2)
  CheckedOut
}
