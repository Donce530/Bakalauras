import 'dart:convert';

import 'package:http/http.dart';
import 'package:reservation_app/Models/interfaces/json_format.dart';
import 'package:http/http.dart' as http;
import 'package:reservation_app/config/environment.dart';
import 'package:shared_preferences/shared_preferences.dart';

class HttpRequests {
  static Future<Response> post(String endpoint, JsonFormat body) async {
    return http.post(
      Uri.http(Environment.apiAddress, endpoint),
      body: jsonEncode(body.toJson()),
      headers: await _getHeaders(),
    );
  }

  static Future<Response> get(String endpoint, [Map<String, String> queryParams]) async {
    final uri = queryParams == null
        ? Uri.http(
            Environment.apiAddress,
            endpoint,
          )
        : Uri.http(Environment.apiAddress, endpoint, queryParams);

    return http.get(
      uri,
      headers: await _getHeaders(),
    );
  }

  static Future<Response> delete(String endpoint, [Map<String, String> queryParams]) async {
    final uri = queryParams == null
        ? Uri.http(
            Environment.apiAddress,
            endpoint,
          )
        : Uri.http(Environment.apiAddress, endpoint, queryParams);

    return http.delete(
      uri,
      headers: await _getHeaders(),
    );
  }

  static Future<Map<String, String>> _getHeaders() async {
    final headers = Environment.jsonHeaders;
    final preferences = await SharedPreferences.getInstance();
    if (preferences.containsKey('token')) {
      final token = preferences.getString('token');
      headers.putIfAbsent('Authorization', () => 'Bearer $token');
    }

    return headers;
  }
}
