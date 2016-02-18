As applications are becoming more and more layered, the need for translation classes as you cross layer
boundaries is increasing. Writing unit tests for translators can be difficult; they always seem to just directly duplicate the translator itself, and never catch half of the problems caused during translation. Particularly as the classes being translated change over time.

The TranslationTester uses reflection to test that the translation is matching the specification and checks that all the properties on the class being translated from are either translated or explicitly excluded.