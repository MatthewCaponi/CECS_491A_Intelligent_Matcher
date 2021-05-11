
  describe('Change Channel', () => {
    context('single value', () => {
      it('go to Group 1', () => {
        cy.visit(global.urlRoute + 'Messaging')

        cy.get('.search').select('1').contains('Jakes Group')
  


      })
    })
  })