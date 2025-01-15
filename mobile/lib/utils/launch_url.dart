import 'package:url_launcher/url_launcher.dart';

Future<void> launchURL(String url) async {
  try {
    final uriURL = Uri.parse(url);
    if (await canLaunchUrl(uriURL)) {
      await launchUrl(uriURL);
    } else {
      throw 'Could not launch $url';
    }
  } catch (e) {
    rethrow;
  }
}
