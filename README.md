# TextExtraction

![build status](https://travis-ci.org/syonoki/TextExtraction.svg?branch=master)

`TextExtraction`은 다양한 문서형태로부터 구조적으로 Text를 추출하기 위한 라이브러리입니다.


## Key component
* ITextObject: 문서파일을 다루기 위한 component입니다. 기본적으로 Word file과 pdf file을 지원하지만 어떤 형태의 문서로도 확장이 가능합니다.
* IExtractionStrategy: 텍스트 추출을 위한 조건을 정의하는 component입니다. 기본적으로 regular expression의 pattern를 이용한 strategy가 제공 되지만 어떠한 형태의 추출 형태로도 확장이 가능합니다.
* ExtractionBlock: ExtractionPipeline을 구성하는 하나의 요소입니다. pipeline상 적용 순서, 이름, ExtractionStrategy를 포함합니다.
* ExtractionPipeline: 문서로부터 특정 텍스트를 추출하는데 일련의 흐름을 정의하여 추출하는 방식을 지원합니다.

## Usage
`TextExtraction`은 다양한 규칙을 통해 반복적으로 문서파일로 부터 텍스트를 추출해야 할때 유용하게 사용할 수 있습니다.
code상에서 또는 다른 data source를 통해 `ExtractionBlock`또는 `ExtractionPipeline`을 정의하고 원하는 텍스트를 추출 할수도 있지만 
[Newtonsoft.Json](http://www.newtonsoft.com/json) 의 json deserialize기능을 통해 object를 직접 생성하는 방식을 추천합니다.

### json ExtractionBlock으로 부터 텍스트 추출

다음과 같은 json type의 ExtractionBlock Definition을 먼저 정의합니다.
```json
{
  "$type": "TextExtration.ExtractionBlock, TextExtration",
  "order": 1,
  "name": "testBlock",
  "extractionStrategy": {
    "$type": "TextExtration.ExtractionStrategy.PatternMatchingExtraction, TextExtration",
    "textPattern": {
      "$type": "TextExtration.ExtractionStrategy.TextExtractionPattern, TextExtration",
      "cutBegin": "test",
      "cutEnd": "test"
    }
  }
}
```

위의 json file을 다음과 같이 읽어와서 extraction block을 생성합니다.
```CSharp
string json = File.ReadAllText("test.json");
var settings = new JsonSerializerSettings() {
	TypeNameHandling = TypeNameHandling.All
};
var block = JsonConvert.DeserializeObject<ExtractionBlock>(json, settings);
```

텍스트를 추출하고 싶은 file을 ITextObject로 생성합니다.
```CSharp
var path = @"testdata\Test document.pdf"
var pdfObject = new PdfTextObject(path);
```

그럼 다음과 같이 ExtractionBlock에 정의된 패턴으로 텍스트 추출을 할 수 있습니다.
```CSharp
var result = block.extract(pdfObject);
```

위와 같은 접근방법으로 json으로 미리 정의된 추출 패턴을 불러옴으로 여러 문서에 반복적으로 패턴 추출을 적용할 수 있습니다.

### json ExtractionBlock으로 부터 텍스트 추출
단순한 형태의 추출의 경우 `ExtractionBlock` 또는 단순히 `IExtractionStrategy`의 구현을 이용하여 텍스트를 구조적으로 추출할 수 있지만 여러 패턴을 동시에 적용하고 싶거나 추출된 텍스트를 구조적으로 변환시키는 작업을 하고 싶으면 `ExtractionPipeline`을 통해 복잡한 추출 흐름을 제어 할 수 있습니다.

```json
{
  "extractionBlocks": [
    {
      "$type": "TextExtration.ExtractionBlock, TextExtration",
      "order": 1,
      "name": "testBlock1",
      "extractionStrategy": {
        "$type": "TextExtration.ExtractionStrategy.PatternMatchingExtraction, TextExtration",
        "textPattern": {
          "$type": "TextExtration.ExtractionStrategy.TextExtractionPattern, TextExtration",
          "cutBegin": "test1",
          "cutEnd": "test1"
        }
      }
    },
    {
      "$type": "TextExtration.ExtractionBlock, TextExtration",
      "order": 2,
      "name": "testBlock2",
      "extractionStrategy": {
        "$type": "TextExtration.ExtractionStrategy.PatternMatchingExtraction, TextExtration",
        "textPattern": {
          "$type": "TextExtration.ExtractionStrategy.TextExtractionPattern, TextExtration",
          "cutBegin": "test2",
          "cutEnd": "test2"
        }
      }
    }],
  "transformationBlocks": [
  {
	"$type": "TextExtration.TransformationBlock, TextExtration",
	"order": 1,
	"name": "testBlock1",
	"targetExtractionId": 1,
	"transformationStrategy": {
		"$type": "TextExtration.TransformationStrategy.RemoveGap, TextExtration"}
	},
    {
  	"$type": "TextExtration.TransformationBlock, TextExtration",
	"order": 2,
	"name": "testBlock2",
	"targetExtractionId": 2,
	"transformationStrategy": {
		"$type": "TextExtration.TransformationStrategy.RemoveGap, TextExtration"}
    }] 
}
```

이때 추출 패턴 정의는 위와같이 복잡해 지지만 추출 과정은 `ExtractionBlock`의 과정과 동일합니다.
```CSharp
string json = File.ReadAllText("test.json");
var settings = new JsonSerializerSettings() {
	TypeNameHandling = TypeNameHandling.All
};
var pipeline = JsonConvert.DeserializeObject<ExtractionPipeline>(json, settings);

var result = pipeline.extract(pdfObject);
```

### PatternMatchingExtraction
`PatternMatchingExtraction`은 regular expression을 이용한 text extraction 방식입니다.
`TextPattern`의 다음의 field를 통해 text 
* cutBegin : 긴 문서의 경우 일부 텍스트 기준으로 자른후 regular expression을 적용하고 싶을때 사용합니다. cutBegin은 자르는 시작 텍스트가 됩니다.
* cutEnd : cutBegin은 자르는 끝 텍스트가 됩니다.
* cutIndex : cutting을 적용시 여러 텍스트조각이 발생시 선택하기 위하는 조각의 index를 지정합니다 
* lookBehind : regex의 lookBehind(?<=)
* lookAhead : regex의 lookAhead (?=)
* pattern : regex의 패턴
