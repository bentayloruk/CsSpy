# CsSpy

Visual Studio 2010 Visualizer for use with Microsoft and Ascentium Commerce Server (2007 & 2009).  Focused on making pipeline component development debugging more pleasant.  For now.

Written by [@enticify](http://twitter.com/enticify) and used in the production of [best discount engine for Commerce Server.](http://www.enticify.com/)

## Features

* [DictionaryClass](http://msdn.microsoft.com/en-us/library/bb509189) visualizer. [Screenshot](https://github.com/enticify/CsSpy/blob/master/README.md#dictionaryclass-visualizer-window).
* [SimpleListClass](http://msdn.microsoft.com/en-us/library/microsoft.commerceserver.runtime.simplelistclass.aspx) visualizer.  [Screenshot](https://github.com/enticify/CsSpy/blob/master/README.md#simplelistclass-visualizer-window).
* Want more?  [Raise feature requests.](https://github.com/enticify/CsSpy/issues)

## Documentation

* Hover mouse over `DictionaryClass` or `SimpleListClass` instances in the VS2010 debugger.
* Click the little magnifying glass.
* Feel the wonder of your data!

## Installation

0. **Super-alpha-ctp version 0.1.  Proceed at your own risk :)**
1. Download [CsSpy-0.1.zip](https://dl.dropbox.com/s/pufnkchsjx600ah/CsSpy-0.1.zip?dl=1).
2. Be good and virus scan it (I did).
2. **Important: Right click zip -> click Properties -> click Unblock -> click OK**
2. Unzip the files and copy to `%USERPROFILE%\Documents\Visual Studio 2010\Visualizers`.  *Per user visualizer location.*
3. DEBUG AWAY!!! **Visual Studio 2010 only.** [*Want 2012 support?*](https://github.com/enticify/CsSpy/issues/2)

## Problems ?

* `System.NotSupportedException: An attempt was made...` -> You probably missed the Unblock step during installation.
* [Raise issues.](https://github.com/enticify/CsSpy/issues)

## Screen Shots

### DictionaryClass Visualizer Window

![CsSpy DictionaryClass Visualizer Window](https://raw.github.com/enticify/CsSpy/master/assets/csspy-dictionary.png)

### SimpleListClass Visualizer Window
![CsSpy SimpleListClass Visualizer Window](https://raw.github.com/enticify/CsSpy/master/assets/csspy-simplelist.png)


## License

[MIT](https://github.com/enticify/CsSpy/blob/master/LICENSE.md)
