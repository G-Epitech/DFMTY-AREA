class TriggerPropertiesFieldsValues {
  final String name;
  final String id;

  TriggerPropertiesFieldsValues({
    required this.name,
    required this.id,
  });
}

class TriggerPropertiesFields {
  final String name;
  final String selectedValue;
  final List<TriggerPropertiesFieldsValues> values;

  TriggerPropertiesFields({
    required this.name,
    required this.selectedValue,
    required this.values,
  });

  TriggerPropertiesFields copyWith({
    String? selectedValue,
  }) {
    return TriggerPropertiesFields(
      name: name,
      selectedValue: selectedValue ?? this.selectedValue,
      values: values,
    );
  }
}
