import { CodeLearnTemplatePage } from './app.po';

describe('CodeLearn App', function() {
  let page: CodeLearnTemplatePage;

  beforeEach(() => {
    page = new CodeLearnTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
