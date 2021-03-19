import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:reservation_app/services/http_requests.dart';

import 'Models/restaurants/restaurant_list_item.dart';

class RestaurantsPage extends StatefulWidget {
  @override
  _RestaurantsPageState createState() => _RestaurantsPageState();
}

class _RestaurantsPageState extends State<RestaurantsPage> {
  List<String> _cities;
  List<RestaurantListItem> _restaurants = [];
  int _page = 0;
  String _city = '';
  String _filter = '';
  var _hasMoreRestaurants = true;
  var _isLoading = false;

  @override
  void initState() {
    super.initState();
    _getCities().then((cities) {
      cities.sort((a, b) => a.compareTo(b));
      setState(() {
        _cities = cities;
        _city = cities[0];
      });
    });
  }

  Future<Widget> _buildListItemAsync(BuildContext context, int i) async {
    if (i.isOdd) {
      return Divider();
    }

    final index = i ~/ 2;
    if (index >= _restaurants.length) {
      if (!_isLoading) {
        _restaurants.addAll(await _getRestaurants(_page, _city, _filter));
        _page++;
      }

      return Center(
        child: SizedBox(
          child: CircularProgressIndicator(),
          height: 24,
          width: 24,
        ),
      );
    }

    return _buildRow(_restaurants[index]);
  }

  Card _buildRow(RestaurantListItem item) {
    return Card(
      child: ListTile(
        title: Text(item.title),
        subtitle: Text(item.address),
        trailing: IconButton(
          icon: Icon(Icons.arrow_right),
          onPressed: () => _onItemClick(item),
        ),
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    if (_cities == null) {
      return Center(
        child: SizedBox(
          child: CircularProgressIndicator(),
          height: 24,
          width: 24,
        ),
      );
    }

    return Column(
      children: [
        Padding(
          child: Row(
            mainAxisAlignment: MainAxisAlignment.start,
            children: [
              Expanded(
                child: Padding(
                  padding: EdgeInsets.only(right: 10, left: 10),
                  child: TextField(
                    onSubmitted: _onFilterSubmitted,
                    decoration: InputDecoration(
                      labelText: 'Paieška',
                    ),
                  ),
                ),
              ),
              IntrinsicWidth(
                child: Padding(
                  padding: EdgeInsets.only(right: 10),
                  child: DropdownButton<String>(
                    itemHeight: 50,
                    value: _city,
                    onChanged: (newCity) {
                      if (_city != newCity) {
                        this._city = newCity;
                        _clearFetchedData();
                      }
                    },
                    items: _cities.map<DropdownMenuItem<String>>((String value) {
                      return DropdownMenuItem<String>(
                        value: value,
                        child: Text(value),
                      );
                    }).toList(),
                  ),
                ),
              ),
            ],
          ),
          padding: EdgeInsets.all(5),
        ),
        Expanded(
          child: ListView.builder(
              padding: EdgeInsets.all(16),
              itemCount: _hasMoreRestaurants ? _restaurants.length + 1 : _restaurants.length,
              itemBuilder: (context, i) {
                return FutureBuilder(
                    future: _buildListItemAsync(context, i),
                    builder: (context, snapshot) {
                      if (snapshot.connectionState == ConnectionState.done) {
                        return snapshot.hasError ? Text(snapshot.error) : snapshot.data;
                      } else {
                        return Center(
                            child: SizedBox(
                          child: CircularProgressIndicator(),
                          height: 24,
                          width: 24,
                        ));
                      }
                    });
              }),
        )
      ],
    );
  }

  void _onFilterSubmitted(String filter) {
    if (filter == _filter) {
      return;
    }

    this._filter = filter;
    _clearFetchedData();
  }

  void _clearFetchedData() {
    setState(() {
      this._hasMoreRestaurants = true;
      this._restaurants = [];
      this._page = 0;
    });
  }

  void _onItemClick(RestaurantListItem item) {}

  Future<List<String>> _getCities() async {
    final url = 'api/Restaurant/AvailableCities';
    final response = await HttpRequests.get(url);

    if (response.statusCode != 200) {
      throw new Exception("Couldn't get cities");
    }

    final result = jsonDecode(response.body) as List<dynamic>;
    final cities = result.cast<String>();
    return cities;
  }

  Future<List<RestaurantListItem>> _getRestaurants(int page, String city, String filter) async {
    final url = '/api/Restaurant/Page';
    _isLoading = true;
    final queryParams = {'page': page.toString(), 'city': city, 'filter': filter};
    final response = await HttpRequests.get(url, queryParams);

    if (response.statusCode != 200) {
      throw new Exception("Couldn't get restaurants");
    }
    final list = jsonDecode(response.body) as List<dynamic>;

    setState(() {
      _hasMoreRestaurants = list.length > 0;
      _isLoading = false;
    });

    return list.map((le) => RestaurantListItem.fromJson(le)).toList();
  }
}
