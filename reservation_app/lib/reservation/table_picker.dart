import 'dart:async';
import 'dart:convert';

import 'package:reservation_app/Models/restaurants/plan/plan_table.dart';
import 'package:reservation_app/Models/utils/plan_drawing_constants.dart';
import 'package:reservation_app/utils/loading_spinner.dart';
import 'package:svg_path_parser/svg_path_parser.dart';

import 'package:flutter/material.dart';
import 'package:reservation_app/Models/restaurants/plan/plan_item_base.dart';
import 'package:reservation_app/Models/restaurants/plan/restaurant_plan.dart';
import 'package:reservation_app/services/http_requests.dart';

class TablePicker extends StatefulWidget {
  const TablePicker(
      {Key key,
      @required this.restaurantId,
      @required this.onTablePicked,
      @required this.availableTableStream})
      : super(key: key);

  final int restaurantId;
  final Function(PlanTable) onTablePicked;
  final Stream<List<int>> availableTableStream;

  @override
  _TablePickerState createState() => _TablePickerState();
}

class _TablePickerState extends State<TablePicker> {
  RestaurantPlan _plan;
  List<int> _availableTableIds = [34, 35, 36];
  int _selectedTableId;
  StreamSubscription<List<int>> _availableTablesSubscription;

  @override
  void initState() {
    _getPlan();
    _availableTablesSubscription = widget.availableTableStream.listen((ids) => setState(() {
          _availableTableIds = ids;
          if (!_availableTableIds.contains(_selectedTableId)) {
            _selectedTableId = null;
            widget.onTablePicked(null);
          }
        }));
    super.initState();
  }

  @override
  void dispose() {
    _availableTablesSubscription.cancel();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    if (_plan == null) {
      return Loader();
    }

    return Container(
      height: 450,
      child: Column(mainAxisSize: MainAxisSize.min, children: [
        _buildPlan(context),
        _buildTableSelector(context),
      ]),
    );
  }

  Future _getPlan() async {
    final url = 'api/Restaurant/Plan/${widget.restaurantId}';
    final response = await HttpRequests.get(url);

    if (response.statusCode != 200) {
      throw new Exception('Could not get plan');
    }

    final plan = RestaurantPlan.fromJson(jsonDecode(response.body));

    setState(() {
      _plan = plan;
      _scalePlan();
    });
  }

  void _scalePlan() {
    final regex = new RegExp(r'd="([ \w]{10,})');
    final drawables = List<PlanItemBase>.from(_plan.walls)..addAll(_plan.tables);
    for (var item in drawables) {
      var buffer = StringBuffer();
      var instructions = regex.firstMatch(item.svg).group(0).substring(4).split(' ').toList();
      for (var i = 0; i < instructions.length; i++) {
        switch (instructions[i]) {
          case 'M':
          case 'L':
            var x = int.parse(instructions[i + 1]) / PlanDrawingConstants.xScaleFactor;
            var y = int.parse(instructions[i + 2]) / PlanDrawingConstants.yScaleFactor;

            buffer.write('${instructions[i]} $x $y ');
            i += 2;
            break;
          case 'A':
            var sx = int.parse(instructions[i + 1]) / PlanDrawingConstants.xScaleFactor;
            var sy = int.parse(instructions[i + 2]) / PlanDrawingConstants.yScaleFactor;
            var xAxisRotation = instructions[i + 3];
            var largeArcFlag = instructions[i + 4];
            var sweepFlag = instructions[i + 5];
            var ex = int.parse(instructions[i + 6]) / PlanDrawingConstants.xScaleFactor;
            var ey = int.parse(instructions[i + 7]) / PlanDrawingConstants.yScaleFactor;
            buffer.write('A $sx $sy $xAxisRotation $largeArcFlag $sweepFlag $ex $ey ');
            i += 7;
            break;
          case 'Z':
            buffer.write('Z');
            break;
        }

        item.scaledSvg = buffer.toString();
      }
    }
  }

  Widget _buildPlan(BuildContext context) {
    return Container(
      color: Colors.transparent,
      alignment: Alignment.center,
      width: double.infinity,
      height: 220,
      child: AspectRatio(
        aspectRatio: 1.6,
        child: Container(
          child: CustomPaint(
            painter: PathPainter(_plan, _availableTableIds, _selectedTableId),
          ),
        ),
      ),
    );
  }

  Widget _buildTableSelector(BuildContext context) {
    return Expanded(
      child: GridView.count(
        primary: false,
        padding: const EdgeInsets.all(10),
        crossAxisSpacing: 5,
        mainAxisSpacing: 5,
        crossAxisCount: 3,
        children: _plan.tables
            .map(
              (table) => _buildGridItem(table),
            )
            .toList(),
      ),
    );
  }

  Widget _buildGridItem(PlanTable table) {
    var stack = Stack(
      children: [
        Card(
          child: InkWell(
            child: Column(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                Padding(
                  padding: EdgeInsets.only(bottom: 8),
                  child: Text(
                    table.number.toString(),
                    style: TextStyle(fontWeight: FontWeight.bold, fontSize: 30),
                  ),
                ),
                Padding(
                  padding: EdgeInsets.symmetric(horizontal: 8),
                  child: Text(
                    'Sėdimų vietų: ${table.seats.toString()}',
                    textAlign: TextAlign.center,
                  ),
                ),
              ],
            ),
            onTap: _availableTableIds.contains(table.id)
                ? () {
                    setState(() {
                      _selectedTableId = table.id;
                      widget.onTablePicked(table);
                    });
                  }
                : null,
          ),
        ),
      ],
    );

    if (!_availableTableIds.contains(table.id)) {
      stack.children.add(
        Align(
          alignment: Alignment.topRight,
          child: Padding(
            padding: EdgeInsets.all(8),
            child: Icon(
              Icons.highlight_off,
              color: Colors.red,
            ),
          ),
        ),
      );
    }

    if (table.id == _selectedTableId) {
      stack.children.add(
        Align(
          alignment: Alignment.topRight,
          child: Padding(
            padding: EdgeInsets.all(8),
            child: Icon(
              Icons.check_circle_outline,
              color: Colors.green,
            ),
          ),
        ),
      );
    }

    return stack;
  }
}

class PathPainter extends CustomPainter {
  final RestaurantPlan plan;
  final List<int> availableTableIds;
  final int selectedId;
  PathPainter(this.plan, this.availableTableIds, this.selectedId);

  @override
  void paint(Canvas canvas, Size size) {
    final drawables = List<PlanItemBase>.from(plan.walls)..addAll(plan.tables);
    for (var item in drawables) {
      var color = Colors.black;
      if (item is PlanTable) {
        color = _getColor(item.id);
      }

      var path = parseSvgPath(item.scaledSvg);
      canvas.drawPath(
          path,
          Paint()
            ..style = PaintingStyle.stroke
            ..color = color
            ..strokeWidth = 2.0);
    }

    var color = Colors.black;
    for (var label in plan.labels) {
      final offset = Offset(
          label.x / PlanDrawingConstants.xScaleFactor +
              label.width / (PlanDrawingConstants.xScaleFactor * 8),
          label.y / PlanDrawingConstants.yScaleFactor +
              label.height / (PlanDrawingConstants.yScaleFactor * 2));
      _paintText(label.text, offset, label.fontSize / PlanDrawingConstants.xScaleFactor, canvas,
          size, color,
          maxWidth: label.width);
    }

    for (var table in plan.tables) {
      Color color = _getColor(table.id);
      final offset = Offset(
          table.x / PlanDrawingConstants.xScaleFactor +
              table.width / (PlanDrawingConstants.xScaleFactor * 2),
          table.y / PlanDrawingConstants.yScaleFactor +
              table.height / (PlanDrawingConstants.yScaleFactor * 2));

      final fontSize = (table.height + table.width) / 8;
      _paintText(table.number.toString(), offset, fontSize, canvas, size, color);
    }
  }

  Color _getColor(int tableId) {
    var color = Colors.black;
    if (tableId == selectedId) {
      color = Colors.green;
    } else if (!availableTableIds.contains(tableId)) {
      color = Colors.red;
    }
    return color;
  }

  @override
  bool shouldRepaint(PathPainter oldDelegate) => true;

  @override
  bool shouldRebuildSemantics(PathPainter oldDelegate) => false;

  void _paintText(
      String text, Offset offset, double fontSize, Canvas canvas, Size size, Color color,
      {double maxWidth}) {
    final textStyle = TextStyle(
      color: color,
      fontSize: fontSize,
    );
    final textSpan = TextSpan(
      text: text,
      style: textStyle,
    );
    final textPainter = TextPainter(
      text: textSpan,
      textDirection: TextDirection.ltr,
      textAlign: TextAlign.center,
    );
    textPainter.layout(minWidth: 0, maxWidth: maxWidth != null ? maxWidth : 0);
    final drawOffset = Offset(offset.dx, offset.dy - (textPainter.height / 2));
    textPainter.paint(canvas, drawOffset);
  }
}
