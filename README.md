# CsSpy

Visual Studio 2010 & 2012 Visualizer for use with Microsoft and Ascentium Commerce Server (2007 & 2009).  Focused on making pipeline component development debugging more pleasant.  For now.

Written by [@enticify](http://twitter.com/enticify) and used in the production of [best discount engine for Commerce Server.](http://www.enticify.com/)

## Features

* [DictionaryClass](http://msdn.microsoft.com/en-us/library/bb509189) visualizer. [Screenshot](https://github.com/enticify/CsSpy/blob/master/README.md#dictionaryclass-visualizer-window).
* [SimpleListClass](http://msdn.microsoft.com/en-us/library/microsoft.commerceserver.runtime.simplelistclass.aspx) visualizer.  [Screenshot](https://github.com/enticify/CsSpy/blob/master/README.md#simplelistclass-visualizer-window).
* Want more?  [Raise feature requests.](https://github.com/enticify/CsSpy/issues)

## Documentation

* Hover mouse over `DictionaryClass` or `SimpleListClass` instances in the VS2010 debugger.
* Click the little magnifying glass.
* Feel the wonder of your data!
* *If you get function time out issues, add the item you wish to visualize to a Watch first and then visualize it.  [Read this for more info.](https://github.com/enticify/CsSpy/issues/5)*

[Release Notes](https://github.com/enticify/CsSpy/blob/master/README.md#release-notes)

## Installation

**Super-alpha-ctp version 0.2.  Proceed at your own risk :)**

### Visual Studio 2010

1. Download [CsSpy-vs2010-0.2.zip](https://dl.dropbox.com/s/spsnjj4qp75nw39/CsSpy-vs2010-0.2.zip?dl=1).
2. **Important: Right click the zip -> click Properties -> click Unblock -> click OK.**  Otherwise, VS wont load it.
2. Unzip the files and copy to `%USERPROFILE%\Documents\Visual Studio 2010\Visualizers`.
3. **DEBUG AWAY!!!**

### Visual Studio 2012

1. Download [CsSpy-vs2012-0.2.zip](https://dl.dropbox.com/s/utotoblkljr7xvs/CsSpy-vs2012-0.2.zip?dl=1).
2. **Important: Right click the zip -> click Properties -> click Unblock -> click OK.**  Otherwise, VS wont load it.
2. Unzip the files and copy to `%USERPROFILE%\Documents\Visual Studio 2012\Visualizers` (will be `Visual Studio 11` on some VS2012 RC machines).
3. **DEBUG AWAY!!!**

## Known Issues

* `Funtion evaluation timed out` - [Workaround and discussion here](https://github.com/enticify/CsSpy/issues/5).
* `System.NotSupportedException: An attempt was made...`? - You probably missed the Unblock step during installation.

[Raise other issues.](https://github.com/enticify/CsSpy/issues)

## Screen Shots

### DictionaryClass Visualizer Window

![CsSpy DictionaryClass Visualizer Window](https://raw.github.com/enticify/CsSpy/master/assets/csspy-dictionary.png)

### SimpleListClass Visualizer Window
![CsSpy SimpleListClass Visualizer Window](https://raw.github.com/enticify/CsSpy/master/assets/csspy-simplelist.png)

## Release Notes

### 0.2 Minor non-interesting update

* Change:  Added exception handler in case of data serialization timeout.

### 0.1 Initial release

* Feature:  DictionaryClass Visualizer.
* Feature:  SimpleListClass Visualizer.

## License

[MIT](https://github.com/enticify/CsSpy/blob/master/LICENSE.md)
