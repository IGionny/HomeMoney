let path = require('path');
let webpack = require('webpack');
const {VueLoaderPlugin} = require('vue-loader');

module.exports = {
  entry: {
    HomeMoney: './Typescript/VueUI/HomeMoney/HomeMoney.ts',
  },
  output: {
    path: path.resolve(__dirname, './wwwroot/VueUI/'),
    publicPath: '/VueUI/',
    filename: '[name].js'
  },
  mode: process.env.NODE_ENV,
  externals: {
    "jquery": "jQuery",
    "vue": 'Vue',
    "vee-validate": "VeeValidate",
    "chart.js": "Chart",
    "moment": "moment",
    "moment-timezone": "moment-timezone",
  },
  resolve: {
    extensions: ['.ts', '.js', '.vue', '.json'],
    alias: {
      'vue$': 'vue/dist/vue.esm.js'
    }
  },
  module: {
    rules: [
      {
        test: /\.vue$/,
        include: path.resolve(__dirname, 'Typescript', "VueUI"),
        exclude: /node_modules/,
        //loader: 'vue-loader'
        use: ["cache-loader", "vue-loader"]
      },
      {
        test: /\.tsx?$/,
        use: [
          "cache-loader",
          {
            loader: "ts-loader",
            options: {
              appendTsSuffixTo: [/\.vue$/]
            }
          }
        ],
//        loader: 'ts-loader',
        include: path.resolve(__dirname, 'Typescript', "VueUI"),
        exclude: /node_modules/,

      },
      {
        test: /\.css$/,
        include: path.resolve(__dirname, 'Typescript', "VueUI"),
        oneOf: [
          // this applies to <style module>
          {
            resourceQuery: /module/,
            use: [
              'vue-style-loader',
              {
                loader: 'css-loader',
                options: {
                  modules: true,
                  localIdentName: '[local]_[hash:base64:8]'
                }
              }
            ]
          },
          // this applies to <style> or <style scoped>
          {
            use: [
              'vue-style-loader',
              'css-loader'
            ]
          }
        ]
      }
      /*,{
          test: /\.(png|jpg|gif|svg)$/,
          loader: 'file-loader',
          options: {
              name: '[name].[ext]?[hash]'
          }
      }*/
      /*{
         test: /\.js$/,
         loader: 'babel-loader',
         exclude: /node_modules/
     },*/
    ]
  },

  devServer: {
    historyApiFallback: true,
    noInfo: true,
    overlay: true
  },
  devtool: 'eval',
  plugins: [
    // make sure to include the plugin for the magic
    new VueLoaderPlugin()
  ]
};
