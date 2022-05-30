module.exports = {
    env: {
        browser: true,
        es2021: true,
    },
    extends: [
        'plugin:react/recommended',
        'airbnb',
    ],
    globals: {
        RequestInit: 'writable',
        RequestRedirect: 'writable',
    },
    parser: '@typescript-eslint/parser',
    parserOptions: {
        ecmaFeatures: {
            jsx: true,
        },
        ecmaVersion: 'latest',
        sourceType: 'module',
    },
    plugins: [
        'import',
        'react',
        '@typescript-eslint',
    ],
    rules: {
        'arrow-parens': 'off',
        'class-methods-use-this': 'off',
        'comma-dangle': [
            'error', {
                arrays: 'always-multiline',
                exports: 'always',
                functions: 'never',
                objects: 'always-multiline',
            },
        ],
        indent: [
            'error',
            4,
        ],
        'func-names': [
            'error',
            'never',
        ],
        'import/extensions': 'off',
        'jsx-quotes': [
            'error',
            'prefer-single',
        ],
        'linebreak-style': [
            'error',
            'windows',
        ],
        'no-unused-vars': 'off',
        'object-curly-newline': [
            'error', {
                ObjectExpression: 'always',
                ObjectPattern: {
                    multiline: true,
                },
                ImportDeclaration: 'never',
            },
        ],
        'react/jsx-filename-extension': [
            'error',
            {
                extensions: [
                    '.tsx',
                ],
            },
        ],
        'react/jsx-indent': [
            'error',
            4,
        ],
        'react/jsx-indent-props': [
            'error',
            4,
        ],
        'react/require-default-props': 'off',
    },
    settings: {
        'import/parsers': {
            '@typescript-eslint/parser': ['.ts', '.tsx'],
        },
        'import/resolver': {
            typescript: {
                alwaysTryTypes: true,
            },
        },
    },
};
