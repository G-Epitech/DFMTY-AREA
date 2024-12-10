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
  final String? selectedValue;
  final List<TriggerPropertiesFieldsValues> values;

  TriggerPropertiesFields({
    required this.name,
    this.selectedValue,
    required this.values,
  });

  get options => values.map((e) => e.name).toList();

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
