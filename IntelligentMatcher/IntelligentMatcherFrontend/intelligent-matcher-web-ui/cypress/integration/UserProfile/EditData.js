  describe('Editing User Data', () => {
    context('single value', () => {
      it('EditData', () => {

        cy.visit(Cypress.env('host') + '/Login')

        cy.get(':nth-child(5) > .ui > input').type(`TestUser1`)

        cy.get(':nth-child(8) > .ui > input').type(`TestPassword1`)

        cy.get('.red').click()




        cy.visit(Cypress.env('host') + '/profile?id=1')


        cy.get('.table > .ui').click()

        cy.get(':nth-child(1) > td > .ui > textarea').type(`Test Jobs`)

        cy.get(':nth-child(2) > td > .ui > textarea').type(`Test Goals`)

        cy.get(':nth-child(3) > td > .ui > textarea').type(`Test Hobbies`)

        cy.get(':nth-child(4) > td > .ui > textarea').type(`Test Intrests`)

        cy.get(':nth-child(5) > td > .ui > input').type(`22`)

        //cy.get(':nth-child(6) > td > #sorting').select('Male')

        cy.get(':nth-child(7) > td > .ui > input').type(`Test Ethnicity`)

        cy.get(':nth-child(8) > td > .ui > input').type(`Test Sexual Orientation`)

        cy.get(':nth-child(9) > td > .ui > input').type(`Test Height`)

        cy.get(':nth-child(11) > td > :nth-child(1)').click()

        cy.get(':nth-child(2) > :nth-child(2) > :nth-child(1) > :nth-child(1) > .grid > .one > :nth-child(1) > div').contains('Account Information Updated')



      })
    })
  })