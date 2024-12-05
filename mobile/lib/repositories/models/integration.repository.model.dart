class IntegrationType {
  final String discord = 'Discord';
  final String gmail = 'Gmail';
}

class Integration {
  final String id;
  final String ownerId;
  final IntegrationType type;
  final bool isValid;
  final Map<String, dynamic> properties;

  Integration({
    required this.id,
    required this.ownerId,
    required this.type,
    required this.isValid,
    required this.properties,
  });

  static fromJson(Map<String, dynamic> map) {
    return Integration(
      id: map['id'],
      ownerId: map['ownerId'],
      type: map['type'],
      isValid: map['isValid'],
      properties: map['properties'],
    );
  }
}
