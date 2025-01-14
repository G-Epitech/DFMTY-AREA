import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/features/automation/bloc/automation_creation_bloc.dart';
import 'package:triggo/app/features/automation/models/input.model.dart';
import 'package:triggo/app/features/automation/view/creation/input.view.dart';
import 'package:triggo/app/routes/custom.router.dart';
import 'package:triggo/app/routes/routes_names.dart';
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
            child: ListView(
              children: [
                AutomationLabelParameterWidget(
                  title: 'Label',
                  previewData: state.cleanedAutomation.label.isNotEmpty
                      ? state.cleanedAutomation.label
                      : null,
                  input: AutomationCreationInputView(
                    type: AutomationInputEnum.text,
                    label: 'Label',
                    placeholder: 'Enter a label',
                    onSave: (value) {
                      context
                          .read<AutomationCreationBloc>()
                          .add(AutomationCreationLabelChanged(label: value));
                    },
                    value: state.cleanedAutomation.label.isNotEmpty
                        ? state.cleanedAutomation.label
                        : null,
                    routeToGoWhenSave: RoutesNames.popOneTime,
                  ),
                ),
                SizedBox(height: 12.0),
                AutomationLabelParameterWidget(
                  title: 'Description',
                  previewData: state.cleanedAutomation.description.isNotEmpty
                      ? state.cleanedAutomation.description
                      : null,
                  input: AutomationCreationInputView(
                    type: AutomationInputEnum.textArea,
                    label: 'Description',
                    placeholder: 'Enter a description',
                    onSave: (value) {
                      context.read<AutomationCreationBloc>().add(
                          AutomationCreationDescriptionChanged(
                              description: value));
                    },
                    value: state.cleanedAutomation.description.isNotEmpty
                        ? state.cleanedAutomation.description
                        : null,
                    routeToGoWhenSave: RoutesNames.popOneTime,
                  ),
                ),
              ],
            ),
          ),
        );
      },
    );
  }
}

class AutomationLabelParameterWidget extends StatelessWidget {
  final String title;
  final String? previewData;
  final Widget input;
  final bool disabled;

  const AutomationLabelParameterWidget({
    super.key,
    required this.title,
    required this.previewData,
    required this.input,
    this.disabled = false,
  });

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      behavior: HitTestBehavior.opaque,
      onTap: () {
        if (disabled) return;
        Navigator.push(context, customScreenBuilder(input));
      },
      child: Container(
        padding: EdgeInsets.all(12.0),
        decoration: BoxDecoration(
          color: disabled ? Colors.grey.shade300 : Colors.white,
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
                    previewData == null ? 'No $title' : previewData!,
                    style: Theme.of(context).textTheme.labelMedium,
                  ),
                ],
              ),
            ),
            SvgPicture.asset(
              "assets/icons/chevron-right.svg",
              width: 24.0,
              height: 24.0,
            ),
          ],
        ),
      ),
    );
  }
}
