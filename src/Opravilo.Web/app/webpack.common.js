const path = require('path');
const htmlWebpackPlugin = require('html-webpack-plugin');

module.exports = {
    entry: './src/index.tsx',
    
    module: {
        rules: [
            {
                test: /\.(ts|tsx)$/,
                enforce: 'pre',
                use: [
                    {
                        options: {
                            eslintPath: require.resolve('eslint'),

                        },
                        loader: require.resolve('eslint-loader'),
                    },
                ],
                exclude: /node_modules/,
            },
            {
                test: /\.tsx?$/,
                use: 'ts-loader',
                exclude: /node_modules/,
            }
        ]
    },
    
    resolve: {
        extensions: ['.tsx', '.ts', '.js']
    },
    
    plugins: [
        new htmlWebpackPlugin({
            filename: "../index.html",
            title: "Development",
            template: "templates/template.html"
        })
    ],
    
    output: {
        filename: 'bundle.js',
        path: path.resolve(__dirname, '../wwwroot/public'),
        clean: true
    },
};