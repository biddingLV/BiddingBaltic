class NewsletterModel {
  constructor(
    public name: string,
    public email: string,
    public categories: string
  ) { }
}

class FormNewsletterModel {
  constructor(
    public name: string,
    public email: string,
    public categories: string
  ) { }
}

export { NewsletterModel, FormNewsletterModel };
