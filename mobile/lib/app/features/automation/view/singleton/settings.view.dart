import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/features/automation/bloc/automation/automation_bloc.dart';
import 'package:triggo/app/features/automation/models/input.model.dart';
import 'package:triggo/app/features/automation/view/singleton/input.view.dart';
import 'package:triggo/app/routes/custom.router.dart';
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
                AutomationLabelParameterWidget(
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
                AutomationLabelParameterWidget(
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
            if (previewData == null)
              Padding(
                padding: const EdgeInsets.symmetric(horizontal: 5.0),
                child: Icon(
                  Icons.warning_amber_rounded,
                  color: Theme.of(context).colorScheme.onError,
                  size: 30,
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
