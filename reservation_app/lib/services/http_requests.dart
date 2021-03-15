import 'dart:convert';

import 'package:http/http.dart';
import 'package:reservation_app/Models/interfaces/json_format.dart';
import 'package:http/http.dart' as http;
import 'package:reservation_app/config/environment.dart';

class HttpRequests {
  static Future<Response> post(String endpoint, JsonFormat body) {
    return http.post(
      Uri.http(Environment.apiAddress, endpoint),
      body: jsonEncode(body.toJson()),
      headers: Environment.jsonHeaders,
    );
  }
}
