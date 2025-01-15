import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/features/login/view/login.view.dart';
import 'package:triggo/app/features/register/view/register.view.dart';
import 'package:triggo/app/routes/custom_auth.router.dart';
import 'package:triggo/app/routes/routes_names.dart';
import 'package:triggo/app/widgets/button.triggo.dart';
import 'package:triggo/app/widgets/navigation_bar.triggo.dart';
import 'package:triggo/mediator/authentication.mediator.dart';

class WelcomeView extends StatefulWidget {
  const WelcomeView({super.key});

  @override
  WelcomeViewState createState() => WelcomeViewState();
}

class WelcomeViewState extends State<WelcomeView> {
  @override
  Widget build(BuildContext context) {
    final AuthenticationMediator authMediator =
        RepositoryProvider.of<AuthenticationMediator>(context);
    return Scaffold(
      body: Container(
        padding: const EdgeInsets.all(20.0),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text(
              'Welcome to',
              style: Theme.of(context).textTheme.titleMedium!.copyWith(
                    fontSize: 45,
                  ),
            ),
            Row(
              mainAxisAlignment: MainAxisAlignment.start,
              children: [
                Text(
                  'Triggo',
                  style: Theme.of(context).textTheme.titleLarge!.copyWith(
                        fontSize: 50,
                        height: 1.0,
                      ),
                ),
                const SizedBox(width: 10),
                SvgPicture.asset(
                  'assets/icons/bubbles.svg',
                  width: 50,
                  height: 50,
                  colorFilter: ColorFilter.mode(
                    Theme.of(context).colorScheme.primary,
                    BlendMode.srcIn,
                  ),
                ),
              ],
            ),
            const SizedBox(height: 40),
            SizedBox(
                width: double.infinity,
                child: TriggoButton(
                  text: "Login",
                  onPressed: () {
                    Navigator.of(context).push(
                      CustomAuthRouter(child: const LoginView()),
                    );
                  },
                )),
            const SizedBox(height: 20),
            SizedBox(
                width: double.infinity,
                child: TriggoButton(
                  text: "Register",
                  onPressed: () {
                    Navigator.of(context).push(
                      CustomAuthRouter(child: const RegisterView()),
                    );
                  },
                )),
            const SizedBox(height: 20),
            Row(
              children: [
                Expanded(child: Divider(color: Colors.grey[300])),
                Padding(
                  padding: const EdgeInsets.symmetric(horizontal: 10),
                  child: Text("or login with",
                      style: Theme.of(context)
                          .textTheme
                          .labelLarge!
                          .copyWith(color: Colors.grey)),
                ),
                Expanded(child: Divider(color: Colors.grey[300])),
              ],
            ),
            const SizedBox(height: 20),
            Row(
              children: [
                Expanded(
                  child: ElevatedButton(
                      onPressed: () async {
                        final bool? connected =
                            await authMediator.authenticateWithGoogle();
                        if (context.mounted && connected != null && connected) {
                          Navigator.pushNamedAndRemoveUntil(
                              context, RoutesNames.home, (route) => false);
                        } else if (context.mounted) {
                          ScaffoldMessenger.of(context)
                            ..removeCurrentSnackBar()
                            ..showSnackBar(
                              const SnackBar(
                                content: Text('Could not authenticate'),
                              ),
                            );
                        }
                      },
                      style: ElevatedButton.styleFrom(
                        backgroundColor: Colors.white,
                        shape: RoundedRectangleBorder(
                          side: BorderSide(
                            color: Colors.grey[300]!,
                            width: 0.5,
                          ),
                          borderRadius: BorderRadius.circular(12.0),
                        ),
                        padding: const EdgeInsets.symmetric(
                            horizontal: 32.0, vertical: 16.0),
                      ),
                      child: Row(
                        mainAxisAlignment: MainAxisAlignment.center,
                        children: [
                          Padding(
                            padding: const EdgeInsets.symmetric(horizontal: 10),
                            child: SvgPicture.asset(
                              'assets/icons/google.svg',
                              width: 30,
                              height: 30,
                            ),
                          ),
                          Text(
                            'Sign In with Google',
                            style: Theme.of(context)
                                .textTheme
                                .labelLarge!
                                .copyWith(
                                  fontSize: 16,
                                ),
                          ),
                        ],
                      )),
                ),
              ],
            ),
          ],
        ),
      ),
    );
  }
}
