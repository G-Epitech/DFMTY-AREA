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
    final args =
        ModalRoute.of(context)!.settings.arguments as Map<String, dynamic>;
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
                  Expanded(
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
                          previewData:
                              state.cleanedAutomation.description.isNotEmpty
                                  ? state.cleanedAutomation.description
                                  : null,
                          input: AutomationInputView(
                            type: AutomationInputType.textArea,
                            label: 'Description',
                            placeholder: 'Enter a description',
                            onSave: (value, humanValue) {
                              context.read<AutomationBloc>().add(
                                  AutomationDescriptionChanged(
                                      description: value));
                            },
                            value:
                                state.cleanedAutomation.description.isNotEmpty
                                    ? state.cleanedAutomation.description
                                    : null,
                            routeToGoWhenSave: RoutesNames.popOneTime,
                          ),
                        ),
                      ],
                    ),
                  ),
                  _DeleteButton(isCreated: isCreated, id: id),
                ],
              ),
            ),
          );
        },
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
