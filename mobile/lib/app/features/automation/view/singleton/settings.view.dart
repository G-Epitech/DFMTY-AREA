import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:formz/formz.dart';
import 'package:triggo/app/features/automation/bloc/automation/automation_bloc.dart';
import 'package:triggo/app/features/automation/models/input.model.dart';
import 'package:triggo/app/features/automation/view/singleton/input.view.dart';
import 'package:triggo/app/features/automation/widgets/input_parameter.dart';
import 'package:triggo/app/routes/routes_names.dart';
import 'package:triggo/app/theme/fonts/fonts.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';

class AutomationSettingsView extends StatelessWidget {
  const AutomationSettingsView({super.key});

  @override
  Widget build(BuildContext context) {
    final args = _extractArguments(context);
    final bool isCreated = args['isCreated'] as bool;
    final String? id = args['id'] as String?;

    return BlocListener<AutomationBloc, AutomationState>(
      listener: _listener,
      child: BlocBuilder<AutomationBloc, AutomationState>(
        builder: (context, state) {
          return BaseScaffold(
            title: 'Settings',
            getBack: true,
            body: Padding(
              padding: const EdgeInsets.all(4.0),
              child: Column(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  _buildInputParameters(context, state),
                  _DeleteButton(isCreated: isCreated, id: id),
                ],
              ),
            ),
          );
        },
      ),
    );
  }

  Map<String, dynamic> _extractArguments(BuildContext context) {
    return ModalRoute.of(context)!.settings.arguments as Map<String, dynamic>;
  }

  Widget _buildInputParameters(BuildContext context, AutomationState state) {
    return Expanded(
      child: ListView(
        children: [
          _buildInputParameter(
            context: context,
            state: state,
            title: 'Label',
            inputType: AutomationInputType.text,
            placeholder: 'Enter a label',
            value: state.cleanedAutomation.label,
            onSave: (value, humanValue) {
              context
                  .read<AutomationBloc>()
                  .add(AutomationLabelChanged(label: value));
            },
          ),
          SizedBox(height: 12.0),
          _buildInputParameter(
            context: context,
            state: state,
            title: 'Description',
            inputType: AutomationInputType.textArea,
            placeholder: 'Enter a description',
            value: state.cleanedAutomation.description,
            onSave: (value, humanValue) {
              context
                  .read<AutomationBloc>()
                  .add(AutomationDescriptionChanged(description: value));
            },
          ),
          SizedBox(height: 12.0),
          _buildInputParameter(
            context: context,
            state: state,
            title: 'Icon',
            inputType: AutomationInputType.icon,
            placeholder: 'Select an icon',
            value: state.cleanedAutomation.icon,
            onSave: (value, humanValue) {
              context
                  .read<AutomationBloc>()
                  .add(AutomationIconChanged(iconUri: value));
            },
            hexColor: state.cleanedAutomation.color,
          ),
          SizedBox(height: 12.0),
          _buildInputParameter(
            context: context,
            state: state,
            title: 'Color',
            inputType: AutomationInputType.radio,
            placeholder: 'Select an icon',
            value: state.cleanedAutomation.color,
            onSave: (value, humanValue) {
              context
                  .read<AutomationBloc>()
                  .add(AutomationColorChanged(color: value));
            },
            hexColor: state.cleanedAutomation.color,
            getOptions: () async {
              return [
                AutomationRadioModel(
                  title: 'Black',
                  description: 'Set the color to black',
                  value: '#000000',
                ),
                AutomationRadioModel(
                  title: 'Red',
                  description: 'Set the color to red',
                  value: '#EE3A3D',
                ),
                AutomationRadioModel(
                  title: 'Green',
                  description: 'Set the color to green',
                  value: '#1DC249',
                ),
                AutomationRadioModel(
                  title: 'Blue',
                  description: 'Set the color to blue',
                  value: '#1D39C2',
                ),
                AutomationRadioModel(
                  title: 'Yellow',
                  description: 'Set the color to yellow',
                  value: '#C9C90C',
                ),
                AutomationRadioModel(
                  title: 'Orange',
                  description: 'Set the color to orange',
                  value: '#EE883A',
                ),
                AutomationRadioModel(
                  title: 'Purple',
                  description: 'Set the color to purple',
                  value: '#913AEE',
                ),
                AutomationRadioModel(
                  title: 'Grey',
                  description: 'Set the color to grey',
                  value: '#AEAEB2',
                ),
              ];
            },
          ),
        ],
      ),
    );
  }

  Widget _buildInputParameter({
    required BuildContext context,
    required AutomationState state,
    required String title,
    required AutomationInputType inputType,
    required String placeholder,
    required String? value,
    required void Function(String, String) onSave,
    Future<List<AutomationRadioModel>> Function()? getOptions,
    String? hexColor,
  }) {
    return AutomationInputParameterWithLabel(
      title: title,
      previewData: value?.isNotEmpty == true ? value : null,
      input: AutomationInputView(
        type: inputType,
        label: title,
        placeholder: placeholder,
        onSave: onSave,
        value: value?.isNotEmpty == true ? value : null,
        routeToGoWhenSave: RoutesNames.popOneTime,
        hexColor: hexColor ?? '#000000',
        getOptions: getOptions,
      ),
    );
  }

  void _listener(context, state) {
    if (state.deletingStatus == FormzSubmissionStatus.success) {
      Navigator.pop(context);
      Navigator.pop(context);
      Navigator.pop(context);
    } else if (state.deletingStatus == FormzSubmissionStatus.inProgress &&
        context.mounted) {
      showDialog(
        context: context,
        barrierDismissible: false,
        builder: (BuildContext context) {
          return Center(
            child: CircularProgressIndicator(),
          );
        },
      );
    } else if (state.deletingStatus == FormzSubmissionStatus.failure &&
        context.mounted) {
      Navigator.pop(context);
      ScaffoldMessenger.of(context)
        ..removeCurrentSnackBar()
        ..showSnackBar(SnackBar(
          content: Text('Automation not deleted'),
          backgroundColor: Theme.of(context).colorScheme.onError,
        ));
    }
  }
}

class _DeleteButton extends StatelessWidget {
  final bool isCreated;
  final String? id;

  const _DeleteButton({required this.isCreated, this.id});

  @override
  Widget build(BuildContext context) {
    if (!isCreated) return Container();
    return Row(
      children: [
        Expanded(
          child: ElevatedButton(
            onPressed: () {
              if (id == null) return;
              final confirm = showDialog(
                context: context,
                builder: (context) {
                  return AlertDialog(
                    title: Text('Delete automation'),
                    content: Text(
                        'Are you sure you want to delete this automation ?'),
                    actions: [
                      TextButton(
                        onPressed: () {
                          Navigator.pop(context, false);
                        },
                        child: Text('Cancel'),
                      ),
                      TextButton(
                        onPressed: () {
                          Navigator.pop(context, true);
                        },
                        child: Text('Delete'),
                      ),
                    ],
                  );
                },
              );
              confirm.then((value) {
                if (value == true && context.mounted) {
                  context.read<AutomationBloc>().add(DeleteAutomation(id!));
                }
              });
            },
            style: ElevatedButton.styleFrom(
              backgroundColor: Colors.white,
              shape: RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(12.0),
                side: BorderSide(
                  color: Theme.of(context).colorScheme.onError,
                  width: 1.0,
                ),
              ),
              padding:
                  const EdgeInsets.symmetric(horizontal: 20.0, vertical: 12.0),
            ),
            child: Text(
              'Delete',
              style: TextStyle(
                color: Theme.of(context).colorScheme.onError,
                fontFamily: containerTitle.fontFamily,
                fontSize: 20,
                fontWeight: containerTitle.fontWeight,
                letterSpacing: containerTitle.letterSpacing,
              ),
            ),
          ),
        )
      ],
    );
  }
}
