import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/features/automation/bloc/automation_creation_bloc.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';

class AutomationCreationSettingsView extends StatelessWidget {
  const AutomationCreationSettingsView({super.key});

  @override
  Widget build(BuildContext context) {
    return BlocBuilder<AutomationCreationBloc, AutomationCreationState>(
      builder: (context, state) {
        return BaseScaffold(
          title: 'Settings',
          getBack: true,
          body: Padding(
            padding: const EdgeInsets.all(4.0),
            child: Column(
              children: [
                CustomLabelWidget(
                  title: 'Label',
                  description: state.automation.label,
                  svgPath: 'assets/icons/chevron-right.svg',
                ),
                SizedBox(height: 12.0),
                CustomLabelWidget(
                  title: 'Description',
                  description: state.automation.description,
                  svgPath: 'assets/icons/chevron-right.svg',
                ),
              ],
            ),
          ),
        );
      },
    );
  }
}

class CustomLabelWidget extends StatelessWidget {
  final String title;
  final String description;
  final String svgPath;

  const CustomLabelWidget({
    super.key,
    required this.title,
    required this.description,
    required this.svgPath,
  });

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: EdgeInsets.all(12.0),
      decoration: BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.circular(8.0),
      ),
      child: Row(
        children: [
          Expanded(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(
                  title,
                  style: Theme.of(context).textTheme.labelLarge!.copyWith(
                        fontWeight: FontWeight.bold,
                      ),
                ),
                Text(
                  description.isEmpty ? 'No $title' : description,
                  style: Theme.of(context).textTheme.labelMedium,
                ),
              ],
            ),
          ),
          SvgPicture.asset(
            svgPath,
            width: 24.0,
            height: 24.0,
          ),
        ],
      ),
    );
  }
}
