import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';
import 'package:triggo/models/automation.model.dart';

class CreateAutomationPage extends StatefulWidget {
  final Automation automation;

  const CreateAutomationPage({super.key, required this.automation});

  @override
  State<CreateAutomationPage> createState() => _IntegrationPageState();
}

class _IntegrationPageState extends State<CreateAutomationPage> {
  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      title: 'Create Automation',
      header: _Header(automation: widget.automation),
      body: _AutomationContainer(automation: widget.automation),
      getBack: true,
    );
  }
}

class _AutomationContainer extends StatelessWidget {
  final Automation automation;

  const _AutomationContainer({required this.automation});

  @override
  Widget build(BuildContext context) {
/*    final AutomationMediator automationMediator =
        RepositoryProvider.of<AutomationMediator>(context);*/

    return Column(
      children: [
        CustomRectangleList(automation: automation),
      ],
    );
  }
}

class _Header extends StatelessWidget {
  final Automation automation;

  const _Header({required this.automation});

  @override
  Widget build(BuildContext context) {
    return Container(
      margin: const EdgeInsets.only(bottom: 8.0),
      child: Row(
        children: [
          GestureDetector(
            onTap: () {
              Navigator.of(context).pop();
            },
            child: Icon(
              Icons.arrow_back,
              size: 26.0,
              weight: 2.0,
            ),
          ),
          SizedBox(width: 10.0),
          Container(
            width: 50,
            height: 50,
            decoration: BoxDecoration(
              color: automation.iconColor,
              borderRadius: BorderRadius.circular(12.0),
            ),
            child: Center(
              child: SvgPicture.asset(
                automation.iconUri,
                width: 25,
                height: 25,
                colorFilter: ColorFilter.mode(
                  Colors.white,
                  BlendMode.srcIn,
                ),
              ),
            ),
          ),
          SizedBox(width: 10.0),
          Text(
            automation.name,
            style: Theme.of(context).textTheme.titleLarge!.copyWith(
                  fontSize: 20.0,
                ),
            maxLines: 1,
            overflow: TextOverflow.ellipsis,
          )
        ],
      ),
    );
  }
}

class CustomRectangleList extends StatelessWidget {
  final Automation automation;

  const CustomRectangleList({super.key, required this.automation});

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        _buildRectangle(
          context,
          icon: Icons.message,
          color: Color(0xFF5865F2),
          text: "Message received in channel",
        ),
        SizedBox(height: 10),
        _buildRectangle(
          context,
          icon: Icons.tag_faces,
          color: Color(0xFF5865F2),
          text: "React to a message",
        ),
      ],
    );
  }

  Widget _buildRectangle(
    BuildContext context, {
    required IconData icon,
    required Color color,
    required String text,
  }) {
    return Container(
      decoration: BoxDecoration(
        border: Border.all(color: Color(0xFF5865F2), width: 2),
        borderRadius: BorderRadius.circular(8),
      ),
      padding: const EdgeInsets.all(12),
      child: Row(
        children: [
          Container(
            width: 40,
            height: 40,
            decoration: BoxDecoration(
              color: color,
              borderRadius: BorderRadius.circular(8),
            ),
            child: Icon(icon, color: Colors.white),
          ),
          SizedBox(width: 10),
          Expanded(
            child: Text(
              text,
              style: Theme.of(context).textTheme.labelLarge?.copyWith(
                    color: Colors.black87,
                  ),
              maxLines: 1,
              overflow: TextOverflow.ellipsis,
            ),
          ),
        ],
      ),
    );
  }
}
