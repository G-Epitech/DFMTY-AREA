import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/features/automation/bloc/automation/automation_bloc.dart';
import 'package:triggo/app/features/automation/models/input.model.dart';
import 'package:triggo/app/features/automation/view/singleton/input.view.dart';
import 'package:triggo/app/features/automation/widgets/input_parameter.dart';
import 'package:triggo/app/routes/routes_names.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';

class AutomationSettingsView extends StatelessWidget {
  const AutomationSettingsView({super.key});

  @override
  Widget build(BuildContext context) {
    return BlocBuilder<AutomationBloc, AutomationState>(
      builder: (context, state) {
        return BaseScaffold(
          title: 'Settings',
          getBack: true,
          body: Padding(
            padding: const EdgeInsets.all(4.0),
            child: ListView(
              children: [
                AutomationInputParameterWithLabel(
                  title: 'Label',
                  previewData: state.cleanedAutomation.label.isNotEmpty
                      ? state.cleanedAutomation.label
                      : null,
                  input: AutomationInputView(
                    type: AutomationInputType.text,
                    label: 'Label',
                    placeholder: 'Enter a label',
                    onSave: (value, humanValue) {
                      context
                          .read<AutomationBloc>()
                          .add(AutomationLabelChanged(label: value));
                    },
                    value: state.cleanedAutomation.label.isNotEmpty
                        ? state.cleanedAutomation.label
                        : null,
                    routeToGoWhenSave: RoutesNames.popOneTime,
                  ),
                ),
                SizedBox(height: 12.0),
                AutomationInputParameterWithLabel(
                  title: 'Description',
                  previewData: state.cleanedAutomation.description.isNotEmpty
                      ? state.cleanedAutomation.description
                      : null,
                  input: AutomationInputView(
                    type: AutomationInputType.textArea,
                    label: 'Description',
                    placeholder: 'Enter a description',
                    onSave: (value, humanValue) {
                      context.read<AutomationBloc>().add(
                          AutomationDescriptionChanged(description: value));
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
