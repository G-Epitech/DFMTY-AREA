import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/features/login/routes/custom.router.dart';
import 'package:triggo/app/features/login/view/login_screen.dart';
import 'package:triggo/app/features/register/view/register_screen.dart';
import 'package:triggo/app/widgets/button.triggo.dart';

class WelcomeScreen extends StatelessWidget {
  const WelcomeScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Container(
        padding: const EdgeInsets.all(12.0),
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
                      ),
                ),
                const SizedBox(width: 10),
                SvgPicture.asset(
                  'assets/icons/bubbles.svg',
                  width: 50,
                  height: 50,
                  color: Theme.of(context).colorScheme.primary,
                ),
              ],
            ),
            const SizedBox(height: 20),
            SizedBox(
                width: double.infinity,
                child: TriggoButton(
                  text: "Login",
                  onPressed: () {
                    Navigator.of(context).push(
                      CustomLoginRouter(child: const LoginScreen()),
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
                      CustomLoginRouter(child: const RegisterScreen()),
                    );
                  },
                )),
          ],
        ),
      ),
    );
  }
}
