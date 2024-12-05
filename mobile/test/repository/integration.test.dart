import 'package:flutter_test/flutter_test.dart';
import 'package:http/http.dart' as http;
import 'package:mockito/mockito.dart';
import 'package:triggo/api/codes.dart';
import 'package:triggo/repositories/integration.repository.dart';

import '../api/call.test.mocks.dart';
import '../api/mock/init.mock.dart';

void integrationRepositoryTests() {
  late MockClient mock;
  late IntegrationRepository repository;

  setUp(() {
    mock = MockClient();
    initMock(mock);
    repository = IntegrationRepository(client: mock);

    when(mock.get(
      Uri.parse('/user/integrations'),
      headers: {'Content-Type': 'application/json'},
    )).thenAnswer((_) async => http.Response(
          '{"pageNumber": 1, "pageSize": 10, "totalPages": 1, "totalRecords": 1, "data": []}',
          200,
          headers: {'Content-Type': 'application/json'},
        ));

    when(mock.get(
      Uri.parse('/user/integrations/?page=1'),
      headers: {'Content-Type': 'application/json'},
    )).thenAnswer((_) async => http.Response(
          '{"pageNumber": 1, "pageSize": 10, "totalPages": 1, "totalRecords": 1, "data": []}',
          200,
          headers: {'Content-Type': 'application/json'},
        ));

    when(mock.get(
      Uri.parse('/user/integrations/?page=1&size=10'),
      headers: {'Content-Type': 'application/json'},
    )).thenAnswer((_) async => http.Response(
          '{"pageNumber": 1, "pageSize": 10, "totalPages": 1, "totalRecords": 1, "data": []}',
          200,
          headers: {'Content-Type': 'application/json'},
        ));
  });

  group('IntegrationRepository', () {
    test('getIntegration success', () async {
      final response = await repository.getIntegrations();

      expect(response.statusCode, equals(Codes.ok));
      expect(response.data?.pageNumber, 1);
      expect(response.data?.pageSize, 10);
      expect(response.data?.totalPages, 1);
      expect(response.data?.totalRecords, 1);
      expect(response.data?.data, isEmpty);
    });

    test('getIntegrationByPage success', () async {
      final response = await repository.getIntegrationByPage(1);

      expect(response.statusCode, equals(Codes.ok));
      expect(response.data?.pageNumber, 1);
      expect(response.data?.pageSize, 10);
      expect(response.data?.totalPages, 1);
      expect(response.data?.totalRecords, 1);
      expect(response.data?.data, isEmpty);
    });

    test('getIntegrationByPageAndSize success', () async {
      final response = await repository.getIntegrationByPageAndSize(1, 10);

      expect(response.statusCode, equals(Codes.ok));
      expect(response.data?.pageNumber, 1);
      expect(response.data?.pageSize, 10);
      expect(response.data?.totalPages, 1);
      expect(response.data?.totalRecords, 1);
      expect(response.data?.data, isEmpty);
    });
  });
}
