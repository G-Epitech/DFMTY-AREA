import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/routes/routes_names.dart';
import 'package:triggo/app/widgets/button.triggo.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';
import 'package:triggo/mediator/authentication.mediator.dart';
import 'package:triggo/mediator/user.mediator.dart';
import 'package:triggo/models/user.model.dart';

class ProfileView extends StatefulWidget {
  const ProfileView({super.key});

  @override
  State<ProfileView> createState() => _ProfileViewState();
}

class _ProfileViewState extends State<ProfileView> {
  @override
  Widget build(BuildContext context) {
    final UserMediator userMediator =
        RepositoryProvider.of<UserMediator>(context);
    final AuthenticationMediator authenticationMediator =
        RepositoryProvider.of<AuthenticationMediator>(context);
    final Future<User> user = userMediator.getUser();

    return BaseScaffold(
      title: 'Profile',
      body: Column(
        children: [
          Row(
            children: [
              Expanded(
                child: _UserProfileWidget(user: user),
              ),
            ],
          ),
          Row(
            children: [
              Expanded(
                child: Padding(
                  padding: const EdgeInsets.symmetric(
                      vertical: 8.0, horizontal: 4.0),
                  child: TriggoButton(
                    text: "Logout",
                    onPressed: () {
                      authenticationMediator.logout();
                      Navigator.pushNamedAndRemoveUntil(
                          context, RoutesNames.welcome, (route) => false);
                    },
                  ),
                ),
              ),
            ],
          )
        ],
      ),
    );
  }
}

class _UserProfileWidget extends StatelessWidget {
  final Future<User> user;

  const _UserProfileWidget({required this.user});

  @override
  Widget build(BuildContext context) {
    return FutureBuilder<User>(
      future: user,
      builder: (context, snapshot) {
        if (snapshot.connectionState == ConnectionState.waiting) {
          return Center(child: CircularProgressIndicator());
        } else if (snapshot.hasError) {
          return Center(child: Text('Error: ${snapshot.error}'));
        } else if (!snapshot.hasData) {
          return const _NoDataView();
        } else {
          return _ProfileViewContent(snapshot: snapshot);
        }
      },
    );
  }
}

class _ProfileViewContent extends StatelessWidget {
  final AsyncSnapshot<User> snapshot;

  const _ProfileViewContent({required this.snapshot});

  @override
  Widget build(BuildContext context) {
    return Card(
      child: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            Container(
              decoration: BoxDecoration(
                shape: BoxShape.circle,
                border: Border.all(
                  width: 2,
                ),
              ),
              child: ClipOval(
                child: Image.network(
                  snapshot.data!.picture,
                  width: 120,
                  height: 120,
                  fit: BoxFit.cover,
                ),
              ),
            ),
            SizedBox(height: 16),
            Text('${snapshot.data!.firstName} ${snapshot.data!.lastName}',
                style: Theme.of(context).textTheme.titleMedium),
            Text(
              snapshot.data!.email,
              style: Theme.of(context).textTheme.labelLarge,
            ),
            SizedBox(height: 16),
          ],
        ),
      ),
    );
  }
}

class _NoDataView extends StatelessWidget {
  const _NoDataView();

  @override
  Widget build(BuildContext context) {
    return Center(
      child: Text('No profile data found',
          textAlign: TextAlign.center,
          style: Theme.of(context).textTheme.titleMedium),
    );
  }
}
