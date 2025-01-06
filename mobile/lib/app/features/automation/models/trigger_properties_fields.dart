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
  final String label;
  final String icon;
  final String? selectedValue;
  final List<TriggerPropertiesFieldsValues> values;

  TriggerPropertiesFields({
    required this.name,
    required this.label,
    required this.icon,
    this.selectedValue,
    required this.values,
  });

  get options => values.map((e) => e.name).toList();

  TriggerPropertiesFields copyWith({
    String? selectedValue,
  }) {
    return TriggerPropertiesFields(
      name: name,
      label: label,
      icon: icon,
      selectedValue: selectedValue ?? this.selectedValue,
      values: values,
    );
  }
}
