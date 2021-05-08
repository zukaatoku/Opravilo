const path = require('path');
const htmlWebpackPlugin = require('html-webpack-plugin');

module.exports = {
    entry: './src/index.ts',
    
    module: {
        rules: [
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
          filename: "../index.html"
      })  
    ],
    
    output: {
        filename: 'bundle.js',
        path: path.resolve(__dirname, '../wwwroot/public'),
        clean: true
    },
};